# Local Development HTTPS Certificate Setup

This documents the commands used on this Windows development machine to
generate the API certificate and the iPhone trust profile for
`192.168.0.111`. Run PowerShell from any directory. The API launch profile
already points to `certs/pillo-api.pem` and `certs/pillo-api-key.pem`.

## Generate the CA, API certificate, and iPhone profile

The following is the PowerShell command sequence used. It writes the CA and
server certificate files to the API `certs` directory, exports the public CA
certificate, and embeds that public certificate in an Apple configuration
profile. The private key files are not included in the profile.

```powershell
$ErrorActionPreference = 'Stop'
$certDir = 'C:\_PetProjects\PilloOrganization\MonolithApi\Api\src\Api\certs'
New-Item -ItemType Directory -Force -Path $certDir | Out-Null

$rootCert = Join-Path $certDir 'pillo-api-rootCA.pem'
$rootKey = Join-Path $certDir 'pillo-api-rootCA-key.pem'
$apiCert = Join-Path $certDir 'pillo-api.pem'
$apiKey = Join-Path $certDir 'pillo-api-key.pem'
$csr = Join-Path $certDir 'pillo-api.csr'
$extensions = Join-Path $env:TEMP 'pillo-api-cert-extensions.cnf'
$profilePath = Join-Path $certDir 'pillo-api-rootCA.mobileconfig'
$rootDer = Join-Path $certDir 'pillo-api-rootCA.cer'
$certPublicPem = Join-Path $env:TEMP 'pillo-api-cert-public.pem'
$certPublicDer = Join-Path $env:TEMP 'pillo-api-cert-public.der'
$keyPublicDer = Join-Path $env:TEMP 'pillo-api-key-public.der'

openssl req -new -x509 -newkey rsa:3072 -sha256 -days 3650 -nodes -keyout $rootKey -out $rootCert -subj '/CN=Pillo API Local Development Root CA' -addext 'basicConstraints=critical,CA:TRUE' -addext 'keyUsage=critical,keyCertSign,cRLSign' -addext 'subjectKeyIdentifier=hash'
if ($LASTEXITCODE -ne 0) { throw 'Root CA generation failed.' }

openssl genpkey -algorithm RSA -pkeyopt rsa_keygen_bits:2048 -out $apiKey
if ($LASTEXITCODE -ne 0) { throw 'API private key generation failed.' }
openssl req -new -key $apiKey -out $csr -subj '/CN=192.168.0.111'
if ($LASTEXITCODE -ne 0) { throw 'API certificate request generation failed.' }

$extensionConfig = @'
[v3_req]
basicConstraints=critical,CA:FALSE
keyUsage=critical,digitalSignature,keyEncipherment
extendedKeyUsage=serverAuth
subjectAltName=@alt_names
[alt_names]
IP.1=192.168.0.111
IP.2=127.0.0.1
DNS.1=localhost
'@
Set-Content -LiteralPath $extensions -Value $extensionConfig -Encoding ASCII
openssl x509 -req -in $csr -CA $rootCert -CAkey $rootKey -CAcreateserial -out $apiCert -days 825 -sha256 -extfile $extensions -extensions v3_req
if ($LASTEXITCODE -ne 0) { throw 'API certificate signing failed.' }

openssl x509 -in $rootCert -outform DER -out $rootDer
if ($LASTEXITCODE -ne 0) { throw 'Root CA DER export failed.' }
$rootBase64 = [Convert]::ToBase64String([IO.File]::ReadAllBytes($rootDer))
$rootPayloadId = [guid]::NewGuid().ToString().ToUpperInvariant()
$profileId = [guid]::NewGuid().ToString().ToUpperInvariant()
$profile = @"
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
  <key>PayloadContent</key>
  <array>
    <dict>
      <key>PayloadContent</key><data>$rootBase64</data>
      <key>PayloadDescription</key><string>Trust the local development CA for the Pillo API.</string>
      <key>PayloadDisplayName</key><string>Pillo API Local Root CA</string>
      <key>PayloadIdentifier</key><string>local.pillo.api.rootca</string>
      <key>PayloadType</key><string>com.apple.security.root</string>
      <key>PayloadUUID</key><string>$rootPayloadId</string>
      <key>PayloadVersion</key><integer>1</integer>
    </dict>
  </array>
  <key>PayloadDescription</key><string>Trust the Pillo local development API certificate.</string>
  <key>PayloadDisplayName</key><string>Pillo API Root CA (Development)</string>
  <key>PayloadIdentifier</key><string>local.pillo.api.rootca.profile</string>
  <key>PayloadRemovalDisallowed</key><false/>
  <key>PayloadType</key><string>Configuration</string>
  <key>PayloadUUID</key><string>$profileId</string>
  <key>PayloadVersion</key><integer>1</integer>
</dict>
</plist>
"@
Set-Content -LiteralPath $profilePath -Value $profile -Encoding ASCII

openssl x509 -in $apiCert -pubkey -noout -out $certPublicPem
if ($LASTEXITCODE -ne 0) { throw 'Could not extract API certificate public key.' }
openssl pkey -pubin -in $certPublicPem -outform DER -out $certPublicDer
if ($LASTEXITCODE -ne 0) { throw 'Could not encode certificate public key.' }
openssl pkey -in $apiKey -pubout -outform DER -out $keyPublicDer
if ($LASTEXITCODE -ne 0) { throw 'Could not extract API private key public part.' }
$certHash = (Get-FileHash -LiteralPath $certPublicDer -Algorithm SHA256).Hash
$keyHash = (Get-FileHash -LiteralPath $keyPublicDer -Algorithm SHA256).Hash
if ($certHash -ne $keyHash) { throw 'API key does not match the issued certificate.' }

openssl verify -CAfile $rootCert $apiCert
if ($LASTEXITCODE -ne 0) { throw 'API certificate chain validation failed.' }
openssl x509 -in $apiCert -noout -subject -issuer -dates -ext subjectAltName
if ($LASTEXITCODE -ne 0) { throw 'Could not inspect API certificate.' }

Remove-Item -LiteralPath $csr,$extensions,$certPublicPem,$certPublicDer,$keyPublicDer -Force
Get-ChildItem -LiteralPath $certDir -File | Select-Object Name,Length
```

## Trust the CA on Windows
These are the commands used to add the public CA certificate to the current
Windows user's Trusted Root Certification Authorities store and verify it:

```powershell
$rootDer = 'C:\_PetProjects\PilloOrganization\MonolithApi\Api\src\Api\certs\pillo-api-rootCA.cer'
& certutil.exe -user -addstore Root $rootDer
if ($LASTEXITCODE -ne 0) { throw "certutil failed with exit code $LASTEXITCODE" }
$thumbprint = (openssl x509 -inform DER -in $rootDer -noout -fingerprint -sha1).Split('=')[-1].Replace(':','')
$trusted = Get-ChildItem 'Cert:\CurrentUser\Root' | Where-Object { $_.Thumbprint -eq $thumbprint }
if (-not $trusted) { throw 'Root certificate is not present in the CurrentUser Root store.' }
```

## Restrict private-key file permissions
These commands were run for the current Windows user:

```powershell
$identity = [System.Security.Principal.WindowsIdentity]::GetCurrent().Name
$certDir = 'C:\_PetProjects\PilloOrganization\MonolithApi\Api\src\Api\certs'
$privateFiles = @((Join-Path $certDir 'pillo-api-key.pem'), (Join-Path $certDir 'pillo-api-rootCA-key.pem'))
foreach ($file in $privateFiles) {
    & icacls.exe $file /inheritance:r /grant:r "$($identity):(F)"
    if ($LASTEXITCODE -ne 0) { throw "Could not restrict permissions on $file" }
}
```

## Install on iPhone

Send this file to the iPhone:

```text
C:\_PetProjects\PilloOrganization\MonolithApi\Api\src\Api\certs\pillo-api-rootCA.mobileconfig
```

Install the downloaded profile in iPhone Settings, then enable full trust under
**Settings → General → About → Certificate Trust Settings**.

## Generated files

- `pillo-api.pem` — API server certificate; configured in `launchSettings.json`.
- `pillo-api-key.pem` — API server private key; keep on the API development PC.
- `pillo-api-rootCA.pem` — Root CA certificate in PEM format.
- `pillo-api-rootCA.cer` — Public root CA certificate in DER format.
- `pillo-api-rootCA.mobileconfig` — iPhone profile containing the public root CA.
- `pillo-api-rootCA-key.pem` — Root CA private key; keep secret and do not send to
  the iPhone.
- `pillo-api-rootCA.srl` — OpenSSL CA serial-number file.
