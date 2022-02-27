using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum enumScene
{
    MainMenu,
    HouseShenXin,
    TownJinDong,
    Loading,
    HouseHuang,
    Clinic,
}

public static class Loader
{
    public static String SceneName(enumScene _scene)
    {
        switch (_scene)
        {
            default:
            case enumScene.MainMenu:        return "主菜单";
            case enumScene.HouseShenXin:    return "申信家";
            case enumScene.HouseHuang:      return "黄阿姨家";
            case enumScene.Clinic:          return "诊所";
            case enumScene.Loading:         return "跳转页";
        }
    }
    /*
     * Toby
     */
    // public Animator transition;
    // public float transitionTime = 1f;
    // // Update is called once per frame
    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.Space))
    //     {
    //       LoadNextLevel();
    //     }
    // }
    //
    // public void LoadNextLevel(){
    //   StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    //
    // }
    //
    // IEnumerator LoadLevel(int levelIndex)
    // {
    //   //Play animation
    //   transition.SetTrigger("Start");
    //
    //   yield return new WaitForSeconds(transitionTime);
    //
    //   SceneManager.LoadScene(levelIndex);
    //
    // }

    /*
     * Hazel
     */


     private static Action onLoaderCallback;

     public static void Load(enumScene scene)
     {
         // Set the loader callback action to load the target scene
         onLoaderCallback = () => {
            SceneManager.LoadScene(scene.ToString());
         };
         // load the loading scene
         SceneManager.LoadScene(enumScene.Loading.ToString());


     }

     public static void LoaderCallback()
     {
         // Triggered after the first Update which lets the screen refresh
         // Execute the loader callback action which will load the target scene
         if (onLoaderCallback != null)
         {
             onLoaderCallback();
             onLoaderCallback = null;
         }
     }

    public static enumScene StringToScene(string _str)
    {
        enumScene scene = enumScene.MainMenu;
        try
        {
            scene = (enumScene)Enum.Parse(typeof(enumScene), _str);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            return scene;
        }

        return scene;
    }

}
