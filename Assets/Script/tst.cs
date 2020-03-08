//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;

//public class tst : MonoBehaviour
//{
//    void Start()
//    {
//        someTst.SaveData();
//    }


//    [System.Serializable]
//    class someTst
//    {
//        int a = 50;
//        int b = 50;
//        int c = 50;
//        int d = 50;
//        int e = 50;
//        int f = 50;
//        int g = 50;
//        int h = 50;
//        int i = 50;
//        string j = "daskjfhsjkadfhsjkdfhjkshfkjsdfhjksdahfjksadf";
//        string k = "daskjfhsjkadfhsjkdfhjkshfkjsdfhjksdahfjksadf";
//        string l = "daskjfhsjkadfhsjkdfhjkshfkjsdfhjksdahfjksadf";
//        string m = "daskjfhsjkadfhsjkdfhjkshfkjsdfhjksdahfjksadf";
//        string n = "daskjfhsjkadfhsjkdfhjkshfkjsdfhjksdahfjksadf";
//        string o = "daskjfhsjkadfhsjkdfhjkshfkjsdfhjksdahfjksadf";
//        string p = "daskjfhsjkadfhsjkdfhjkshfkjsdfhjksdahfjksadf";
//        string q = "daskjfhsjkadfhsjkdfhjkshfkjsdfhjksdahfjksadf";
//        string r = "daskjfhsjkadfhsjkdfhjkshfkjsdfhjksdahfjksadf";
//        public static someTst Obj;
//        public static void SaveData()
//        {
//            //if (!Directory.Exists("Saves"))
//            //    Directory.CreateDirectory("Saves");
//            Obj = new someTst();

//            BinaryFormatter formatter = new BinaryFormatter();
//            FileStream saveFile = File.Create("Assets/save5.binary");

//            ////LocalCopyOfData = PlayerState.Instance.localPlayerData;

//            formatter.Serialize(saveFile, Obj);

//            saveFile.Close();


//            //var formatter = new BinaryFormatter();
//            //var stream = new FileStream("Assets/" + "GD.prog", FileMode.Create);
//            //formatter.Serialize(stream, this);
//            //stream.Close();
//        }

//        public void LoadData()
//        {
//            BinaryFormatter formatter = new BinaryFormatter();
//            FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);
//        }
//        public void LoadData1()
//        {
//            BinaryFormatter formatter = new BinaryFormatter();
//            FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);
//        }
//        public void LoadData2()
//        {
//            BinaryFormatter formatter = new BinaryFormatter();
//            FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);
//        }
//        public void LoadData3()
//        {
//            BinaryFormatter formatter = new BinaryFormatter();
//            FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);
//        }
//        public void LoadData4()
//        {
//            BinaryFormatter formatter = new BinaryFormatter();
//            FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);
//        }
//        public void LoadDat5()
//        {
//            BinaryFormatter formatter = new BinaryFormatter();
//            FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);
//        }
//        public void LoadData6()
//        {
//            BinaryFormatter formatter = new BinaryFormatter();
//            FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);
//        }
//        public void LoadData7()
//        {
//            BinaryFormatter formatter = new BinaryFormatter();
//            FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);
//        }
//        public void L8oadData()
//        {
//            BinaryFormatter formatter = new BinaryFormatter();
//            FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);
//        }
//        public void Lo9adData()
//        {
//            BinaryFormatter formatter = new BinaryFormatter();
//            FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);
//        }
//        public void Loa10dData()
//        {
//            BinaryFormatter formatter = new BinaryFormatter();
//            FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);
//        }
//        public void LoadD11ata()
//        {
//            BinaryFormatter formatter = new BinaryFormatter();
//            FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);
//        }
//        public void LoadDat12a()
//        {
//            BinaryFormatter formatter = new BinaryFormatter();
//            FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);
//        }
//        public void L13oadData()
//        {
//            BinaryFormatter formatter = new BinaryFormatter();
//            FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);
//        }
//        public void Loa14dData()
//        {
//            BinaryFormatter formatter = new BinaryFormatter();
//            FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);
//        }


//        public static void ss()
//        {
//            RaycastHit hit;
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            if (Physics.Raycast(ray, out hit, 999, 1 << 8))
//            {
//                var poz = new Vector3(hit.point.x, .5f, hit.point.z);

//                if (
//                    Input.GetMouseButtonDown(0)
//                    )
//                {
//                }
//            }

//        }


//        public static void ss1()
//        {
//            RaycastHit hit;
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            if (Physics.Raycast(ray, out hit, 999, 1 << 8))
//            {
//                var poz = new Vector3(hit.point.x, .5f, hit.point.z);

//                if (
//                    Input.GetMouseButtonDown(0)
//                    )
//                {
//                }
//            }

//        }


//        public static void ss2()
//        {
//            RaycastHit hit;
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            if (Physics.Raycast(ray, out hit, 999, 1 << 8))
//            {
//                var poz = new Vector3(hit.point.x, .5f, hit.point.z);

//                if (
//                    Input.GetMouseButtonDown(0)
//                    )
//                {
//                }
//            }

//        }


//        public static void ss3()
//        {
//            RaycastHit hit;
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            if (Physics.Raycast(ray, out hit, 999, 1 << 8))
//            {
//                var poz = new Vector3(hit.point.x, .5f, hit.point.z);

//                if (
//                    Input.GetMouseButtonDown(0)
//                    )
//                {
//                }
//            }

//        }


//        public static void ss4()
//        {
//            RaycastHit hit;
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            if (Physics.Raycast(ray, out hit, 999, 1 << 8))
//            {
//                var poz = new Vector3(hit.point.x, .5f, hit.point.z);

//                if (
//                    Input.GetMouseButtonDown(0)
//                    )
//                {
//                }
//            }

//        }


//        public static void ss5()
//        {
//            RaycastHit hit;
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            if (Physics.Raycast(ray, out hit, 999, 1 << 8))
//            {
//                var poz = new Vector3(hit.point.x, .5f, hit.point.z);

//                if (
//                    Input.GetMouseButtonDown(0)
//                    )
//                {
//                }
//            }

//        }


//        public static void ss6()
//        {
//            RaycastHit hit;
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            if (Physics.Raycast(ray, out hit, 999, 1 << 8))
//            {
//                var poz = new Vector3(hit.point.x, .5f, hit.point.z);

//                if (
//                    Input.GetMouseButtonDown(0)
//                    )
//                {
//                }
//            }

//        }

//        public void ss7()
//        {
//            RaycastHit hit;
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            if (Physics.Raycast(ray, out hit, 999, 1 << 8))
//            {
//                var poz = new Vector3(hit.point.x, .5f, hit.point.z);

//                if (
//                    Input.GetMouseButtonDown(0)
//                    )
//                {
//                }
//            }

//        }
//        public void ss8()
//        {
//            RaycastHit hit;
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            if (Physics.Raycast(ray, out hit, 999, 1 << 8))
//            {
//                var poz = new Vector3(hit.point.x, .5f, hit.point.z);

//                if (
//                    Input.GetMouseButtonDown(0)
//                    )
//                {
//                }
//            }

//        }
//        public void ss9()
//        {
//            RaycastHit hit;
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            if (Physics.Raycast(ray, out hit, 999, 1 << 8))
//            {
//                var poz = new Vector3(hit.point.x, .5f, hit.point.z);

//                if (
//                    Input.GetMouseButtonDown(0)
//                    )
//                {
//                }
//            }

//        }
//        public void ss10()
//        {
//            RaycastHit hit;
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            if (Physics.Raycast(ray, out hit, 999, 1 << 8))
//            {
//                var poz = new Vector3(hit.point.x, .5f, hit.point.z);

//                if (
//                    Input.GetMouseButtonDown(0)
//                    )
//                {
//                }
//            }

//        }
//        public void ss11()
//        {
//            RaycastHit hit;
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            if (Physics.Raycast(ray, out hit, 999, 1 << 8))
//            {
//                var poz = new Vector3(hit.point.x, .5f, hit.point.z);

//                if (
//                    Input.GetMouseButtonDown(0)
//                    )
//                {
//                }
//            }

//        }
//        public void ss12()
//        {
//            RaycastHit hit;
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            if (Physics.Raycast(ray, out hit, 999, 1 << 8))
//            {
//                var poz = new Vector3(hit.point.x, .5f, hit.point.z);

//                if (
//                    Input.GetMouseButtonDown(0)
//                    )
//                {
//                }
//            }

//        }



//    }

//    //}
//    //public void LoadData()
//    //{
//    //    BinaryFormatter formatter = new BinaryFormatter();
//    //    FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);

//    //    LocalCopyOfData = (PlayerStatistics)formatter.Deserialize(saveFile);

//    //    saveFile.Close();
//    //}

//}
