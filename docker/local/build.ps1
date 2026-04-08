docker compose `
  --profile applications `
  -f .\compose.yml `
  -f .\compose.storage.yml `
  -f .\compose.services.yml `
  -f .\compose.setup.yml `
  -f .\compose.apps.yml `
  build