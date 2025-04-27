# VibraSymphony_Core
VibraSymphonyの中核となるプログラム部分のみを抜粋した簡易版です。チームメンバーのオリジナルデザイン（3DモデルやVFXなど）は除外されています。

---

## 本リポジトリについて

本リポジトリは、チーム開発プロジェクト「VibraSymphony」のコアとなるプログラムロジック部分のみを抜粋・再構成した簡易版です。  
3DモデルやVFXなどのデザイン資産は著作権の都合上、削除または簡易素材に置き換えています。

プロジェクトの詳細については、以下の紹介用リポジトリをご参照ください：  
**[VibraSymphony プロジェクト紹介ページ](https://github.com/rickyKunn/VibraSymphony_Description)**

---

## 連携プロジェクトについて（依存関係）

本リポジトリは、 **別リポジトリ「VibraSymphony_MobileAgent」との連携が必要不可欠です。**

### 関連リポジトリ

**[VibraSymphony_MobileAgent](https://github.com/rickyKunn/VibraSymphony_MobileAgent)**  



### 役割と連携内容

**VibraSymphony_MobileAgent** は、モバイルデバイスと本プロジェクトをリアルタイムで接続するためのアプリケーションです。

主な連携内容は以下の通りです：

- Android デバイスから MP3 ファイルを TCP/IP 経由で送信
- Core 側がその MP3 を再生・FFT解析し、ドラムやベースなどの成分を検出
- 検出結果を OSC (Open Sound Control) を用いて Android デバイスに送信
- Android 側で受信した信号に基づいて、リアルタイムでバイブレーションを発生

これにより、音楽信号と振動が同期した体験をユーザーに提供します。

---

## 利用手順

1. Android 実機上で **VibraSymphony_MobileAgent(以下、「MobileAgent」と略記)** を起動します（Unityフォルダを実機にビルド）。

2. Unity エディタ(または実行ファイル)で **VibraSymphony_Core(以下、「Core」と略記)** を開き、StartSceneを再生します(実行ファイルの場合自動で再生される)。

3. `MobileAgent`で `Main` ボタンを選択

4. **MobileAgent** で画面左下に表示された `ID` を **Core** の `Devices` を追加し入力

5. **Core** の `GO!!` ボタンを選択
   
6. **MobileAgent**で `Pick Music` から曲を選択(Androidのダウンロードフォルダに入っている曲の一覧が表示されます)

(手順5,6 はどちらからでも構いません)

8. 曲の送受信等の通信が完了すると自動でシーンが再生されます


- 本システムは、**CoreとMobileAgentが同一Wi-Fiネットワーク上にあることが必須です。**
- 使用している TCP/IP や OSC プロトコルが **制限されているネットワーク（例：学内ネットワーク、ゲストWi-Fiなど）では通信が確立できず、動作しません。**
- 通信状況が不安定な環境では、タイムラグや反応遅延が発生する可能性があります。**快適なプレイには安定した通信環境を強く推奨します。**
- 何か不明な点、エラー等が発生した場合、**kobayashiritsuki@gmail.com**までご連絡ください。  


---

## ライセンスおよびクレジット

このリポジトリは、ポートフォリオおよび選考目的での閲覧専用として公開しています。

- 商用利用は禁止です  
- 改変や再配布は禁止です  
- 閲覧および参照は自由ですが、許可なく他用途に使用しないでください

その他の目的での使用を希望される場合は、下記までご連絡ください。  
**kobayashiritsuki@gmail.com**

---

本プロジェクトでは、ユニティ・テクノロジーズ・ジャパンが提供する「Unity-Chan」キャラクターおよび関連アセットを使用しています。  
使用にあたっては、以下のライセンスに従っています：  
[Unity-Chan ライセンス（日本語）](https://unity-chan.com/contents/license_jp/)  
[Unity-Chan License (English)](https://unity-chan.com/contents/license_en/)

© 2025 小林立樹  
© Unity Technologies Japan / Unity-Chan Project. All rights reserved.

