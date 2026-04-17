#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
SAMPLE_FILE="$ROOT_DIR/src/FunctionApp/local.settings.sample.json"
TARGET_FILE="$ROOT_DIR/src/FunctionApp/local.settings.json"

if [[ -f "$TARGET_FILE" ]]; then
  echo "local.settings.json already exists at $TARGET_FILE"
  exit 0
fi

cp "$SAMPLE_FILE" "$TARGET_FILE"
echo "Created $TARGET_FILE from sample."
echo "Update Postgres__ConnectionString and AzureWebJobsStorage before running the app."
