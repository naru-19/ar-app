# 家具の３dモデル

instant nerfを使って、３dモデルを作ります。
instant nerf の詳細を[こちら](https://github.com/NVlabs/instant-ngp)に参考してください

動画を写真に変換するコードを加えた
AR-APP/Assets/3DMODEL/scripts/colmap2nerf.py

## 3d model を作る手順

1、動画を撮影する
２、撮影した動画をAR-APP/Assets/3DMODEL/scripts/colmap2nerf.py に入力して、複数の位置から撮影したの画像を作る

```
$ python .\scripts\convert_video.py --input ...\
                                  --output ...\
                                  --show_image 1 \
                                  --scale 2
```

３、NeRF のニューラルネットワークには、シーンを複数の位置から撮影した写真と、その各写真のカメラ位置の情報が必要なので、複数のの画像を colmap でカメラの情報を計算する。（GUI）

`colmap gui`

4、instant nerfに必要の .jsonファイルを作る

```

python scripts/colmap2nerf.py --aabb_scale 16 \
                              --images ... \
                              --text ... \
                              --out ...

```

5、カメラの情報と画像をNeRFのニューラルネットワークに入力して、3dモデルを生成する

`.\build\testbed.exe --scene ...`
