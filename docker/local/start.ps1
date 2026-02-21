param (
	[Parameter(Mandatory=$true)]
	[String]$ProfileName
)

docker compose `
  --profile $ProfileName `
  -f .\compose.yml `
  -f .\compose.storage.yml `
  -f .\compose.services.yml `
  -f .\compose.setup.yml `
  -f .\compose.apps.yml `
  up -d