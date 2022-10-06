# ar-app

3 次元家具モデルを扱った AR アプリ

## 実装パイプライン

<img src="imgs/overview.png">

## 開発の役立つリンク・tips

[github でのチーム開発](https://qiita.com/siida36/items/880d92559af9bd245c34)<br>
[commit message の書き方](https://qiita.com/itosho/items/9565c6ad2ffc24c09364)<-これのライト版くらい書いてりゃ ok

## 開発ルール

### unity 上

- 自分の作った object 以外は触らない．(コードのアタッチも含め)
  - 自分用のシーンを作ると良いかも

### coding ルール

コードフォーマットは Omnisharp を使う.

| 変数例          | 意味                         |
| --------------- | ---------------------------- |
| CameraSwitcher  | class を表す                 |
| camera_state    | 普通の変数だけど，長いやつ． |
| GLOVAL_VARIABLE | グローバル変数は全部大文字   |
| isCamera        | bool 値                      |

## 各種バージョン

| ライブラリ等 | バージョン   |
| ------------ | ------------ |
| unity        | 2021.3.11.f1 |

## TODO

- [ ] all in one な Docker image の作成
  - [ ] python のバージョン
  - [ ] ライブラリのバージョン(特に open3D,formatter は version で挙動が違う)
- [ ] unity install (全員?)
