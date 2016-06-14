#! /bin/sh

project="pacman-unity"

echo "Attempting to build $project for Web"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $(pwd)/unity.log \
  -projectPath $(pwd) \
  -buildWebPlayer "$(pwd)/Deploy/web" \
  -quit

echo 'Logs from build'
cat $(pwd)/unity.log
