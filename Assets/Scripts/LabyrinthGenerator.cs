﻿using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class LabyrinthGenerator : MonoBehaviour
{
    //上から見て縦、Z軸のオブジェクトの量
    public int vertical = 15;
    //上から見て横、X軸のオブジェクトの量
    public int horizontal = 15;

    //Prefabを入れる欄を作る
    public GameObject cube;

    //for文でオブジェクトを縦横に並べるための変数
    int vi;
    int hi;

    //MinerのPrefabを入れるための変数
    public GameObject miner;

    void Start()
    {
        //Cubeを並べるための基準になる位置
        Vector3 pos = new Vector3(0, 0, 0);

        //Z軸にverticalの数だけ並べる
        for (vi = 0; vi < vertical; vi++)
        {
            //X軸にhorizontalの数だけ並べる
            for (hi = 0; hi < horizontal; hi++)
            {
                //PrefabのCubeを生成する
                GameObject copy = Instantiate(cube,
                    //生成したものを配置する位置
                    new Vector3(
                        //X軸
                        pos.x + hi,
                        //Y軸
                        pos.y,
                        //Z軸
                        pos.z + vi
                    //Quaternion.identityは無回転を指定する
                    ), Quaternion.identity);

                //生成したオブジェクトに番号の名前をつける
                copy.name = vi + "-" + hi.ToString();
            }
        }

        //ランダムな数字を縦横分の2つ出す
        //0からだが、並ぶオブジェクトの内側から選びたいので1からにした
        int ver1 = Random.Range(1, vertical - 1);
        int hor1 = Random.Range(1, horizontal - 1);

        //ランダムな数字からオブジェクトを検索してDestroyで消す
        GameObject start = GameObject.Find(ver1 + "-" + hor1);
        Destroy(start);
        //その位置をコンソールに表示
        Debug.Log(start);

        //Minerを生成
        GameObject minerObj = Instantiate(miner, Vector3.zero, Quaternion.identity);
        //MinerオブジェクトのMinerスクリプトを取得
        Miner minerScr = minerObj.GetComponent<Miner>();
        //MinerスクリプトのMining関数に引数を送って実行させる
        minerScr.DoMining(ver1, hor1);
    }
}
