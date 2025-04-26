#!/bin/bash

LIMIT=$((100 * 1024 * 1024))  # 100MB

# OS検出（uname出力で判断）
OS_TYPE=$(uname)

# statのコマンド定義（macOS or Linux）
if [[ "$OS_TYPE" == "Darwin" ]]; then
    STAT_CMD='stat -f%z'  # macOS
elif [[ "$OS_TYPE" == "Linux" ]]; then
    STAT_CMD='stat -c%s'  # Linux
else
    echo "Unsupported OS: $OS_TYPE"
    exit 1
fi

echo " Scanning for files larger than 100MB..."

# Git管理下のファイルを対象にスキャン
git ls-files | while read file; do
    if [ -f "$file" ]; then
        size=$($STAT_CMD "$file")
        if [ "$size" -gt "$LIMIT" ]; then
            echo "📦 Tracking large file with Git LFS: $file"
            git lfs track "$file"
        fi
    fi
done

echo "Done. Don't forget to commit the updated .gitattributes:"
echo "   git add .gitattributes && git commit -m 'Track large files with Git LFS'"
