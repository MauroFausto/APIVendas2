# Create Loki directories if they don't exist
New-Item -ItemType Directory -Force -Path ".\loki\index"
New-Item -ItemType Directory -Force -Path ".\loki\chunks"

# Start Loki with the configuration file
.\loki-windows-amd64.exe --config.file=loki-config.yaml 