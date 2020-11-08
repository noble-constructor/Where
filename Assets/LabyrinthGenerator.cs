using System.Collections;
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

    private List[,] initial()
    {
        int[,] wall = new int[vertical, horizontal];

        for (int v = 1; v <= vertical; v++)
        {
            for (int h = 1; h <= horizontal; h++)
            {
                wall[v, h] = 1
            }
        }
        return wall;
    }

    private List[,] final(List[,] wall)
    {
        for (int v= 1; v <= vertical; v++)
        {

            wall[i, 0] = 1;
            wall[i, vertical - 1] = 1;
        }

        for(int h = 1; h <= horizontal; h++)
        {
            wall[0, i] = 1;
            wall[horizontal - 1, i] = 1;
        }
        return wall;
    }

    private List[,] dig(labyrinth)
    {
        Random cRandom = new System.Random();
        while (true)
        {
            //掘る方向を決める
            int dir = cRandom.Next(4);
            int X;
            int Y;

            //再帰的に掘る
            while (true)
            {
                (X, Y) = Map_dig(labyrinth, x, y, dir);
                if (X == 0 && Y == 0)
                {
                    break;
                }
                x = X;
                y = Y;
                dir = cRandom.Next(4);
            }

            //まだ掘れる場所が存在するか判断

            if (Program.cnt == (maze_length - 1) * (maze_length - 1) / 4)
            {
                break;
            }

            //存在すれば別の座標を決定
            do
            {
                x = 2 * cRandom.Next((maze_length - 1) / 2) + 1;
                y = 2 * cRandom.Next((maze_length - 1) / 2) + 1;
            } while (labyrinth[x, y] == 1);
        }
        return labyrinth;
    }

    //2マス先まで掘れるか判断して掘る。無理なら(0, 0)を返す
    private (int a, int b) Map_dig(int[,] wall, int x, int y, int dir)
    {
        if (dir == 0 && wall[x, y + 1] == 1)
        {
            if (wall[x, y + 2] == 1)
            {
                wall[x, y + 1] = 0;
                wall[x, y + 2] = 0;
                Program.cnt += 1;
                return (x, y + 2);
            }
            else
            {
                return (0, 0);
            }
        }
        else
        {
            if (dir == 1 && wall[x + 1, y] == 1)
            {
                if (wall[x + 2, y] == 1)
                {
                    wall[x + 1, y] = 0;
                    wall[x + 2, y] = 0;
                    Program.cnt += 1;
                    return (x + 2, y);
                }
                else
                {
                    return (0, 0);
                }
            }
            else
            {
                if (dir == 2 && wall[x, y - 1] == 1)
                {
                    if (wall[x, y - 2] == 1)
                    {
                        wall[x, y - 1] = 0;
                        wall[x, y - 2] = 0;
                        Program.cnt += 1;
                        return (x, y - 2);
                    }
                    else
                    {
                        return (0, 0);
                    }
                }
                if (dir == 3 && wall[x - 1, y] == 1)
                {
                    if (wall[x - 2, y] == 1)
                    {
                        wall[x - 1, y] = 0;
                        wall[x - 2, y] = 0;
                        Program.cnt += 1;
                        return (x - 2, y);
                    }
                    else
                    {
                        return (0, 0);
                    }
                }
                else
                {
                    return (0, 0);
                }
            }
        }
    }

    void Start()
    {
        int[,] labyrinth = initial();
        labyrinth = dig(labyrinth);
        labyrinth = final(labyrinth);
    }

    //void Start()
    //{
    //    //Cubeを並べるための基準になる位置
    //    Vector3 pos = new Vector3(0, 0, 0);

    //    //Z軸にverticalの数だけ並べる
    //    for (vi = 0; vi < vertical; vi++)
    //    {
    //        //X軸にhorizontalの数だけ並べる
    //        for (hi = 0; hi < horizontal; hi++)
    //        {
    //            //PrefabのCubeを生成する
    //            GameObject copy = Instantiate(cube,
    //                //生成したものを配置する位置
    //                new Vector3(
    //                    //X軸
    //                    pos.x + hi,
    //                    //Y軸
    //                    pos.y,
    //                    //Z軸
    //                    pos.z + vi
    //                //Quaternion.identityは無回転を指定する
    //                ), Quaternion.identity);

    //            //生成したオブジェクトに番号の名前をつける
    //            copy.name = vi + "-" + hi.ToString();
    //        }
    //    }

    //    //ランダムな数字を縦横分の2つ出す
    //    //0からだが、並ぶオブジェクトの内側から選びたいので1からにした
    //    int ver1 = Random.Range(1, vertical - 1);
    //    int hor1 = Random.Range(1, horizontal - 1);

    //    //ランダムな数字からオブジェクトを検索してDestroyで消す
    //    GameObject start = GameObject.Find(ver1 + "-" + hor1);
    //    Destroy(start);
    //    //その位置をコンソールに表示
    //    Debug.Log(start);

    //    //Minerを生成
    //    GameObject minerObj = Instantiate(miner, Vector3.zero, Quaternion.identity);
    //    //MinerオブジェクトのMinerスクリプトを取得
    //    Miner minerScr = minerObj.GetComponent<Miner>();
    //    //MinerスクリプトのMining関数に引数を送って実行させる
    //    minerScr.DoMining(ver1, hor1);
    //}
}
