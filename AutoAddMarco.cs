
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
public class AutoAddMarco
{

    [MenuItem("Build/AutoAddMarco")]
    static void AutoAddMarcoFiles()
    {
        
        string Path = Application.dataPath + "/SenseAR";
        Debug.LogError(Path);

        SearchDir(Path);
        Debug.LogError("SUCCCCC");
    }

     static void SearchDir(string SourcePath)
    {
       
        // 如果源目录不存在，则退出
        if (!Directory.Exists(SourcePath))
        {
            return;
        }

       
        if (Directory.Exists(SourcePath))
        {

            // 遍历源路径的文件夹，获取文件名（带路径的）
            foreach (string FileName in Directory.GetFiles(SourcePath))
            {
                if(FileName.EndsWith(".cs"))
                {
                    string content = File.ReadAllText(FileName);
                    string prefix = "#if  UNITY_ANDROID || UNITY_EDITOR \n";
                    if(content.Contains(prefix) == false)
                    {
                        content = prefix + content + "\n #endif";
                        File.WriteAllText(FileName, content); 
                    }
                }
            }

            // 子文件夹的遍历

            foreach (string SubPath in Directory.GetDirectories(SourcePath))
            {


                //复制文件
                SearchDir(SubPath);
            }
        }
    }


}
#endif