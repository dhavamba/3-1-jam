using UnityEngine;
using System.Collections.Generic;
using System;

//Base 75 Reimplimentation of the Unity tag system to allow for multiple tags on the same game object.

[SerializableAttribute]
public class MultiTags : MonoBehaviour
{
	[Tooltip("This is the list of current Tags on this GameObject")]
	public List<MT> localTagList = new List<MT>();


    /// <summary>
    /// Create GameObject with MultiTags component
    /// </summary>
    public static GameObject CreateMultiTagsGameObject()
    {
        GameObject obj = new GameObject();
        obj.AddComponent<MultiTags>();
        return obj;
    }

    /// <summary>
    /// Search gameobjects with tags extension (only = gameobject with ONLY specifc tags).
    /// </summary>
    public static GameObject[] FindGameObjectsWithMultiTags(bool only, params string[] tags)
    {
        MultiTags[] tempMT = GameObject.FindObjectsOfType(typeof(MultiTags)) as MultiTags[];
        List<GameObject> tempGOList = new List<GameObject>();

        foreach (MultiTags itemMT in tempMT)
        {
            if (!only || itemMT.localTagList.Count == tags.Length)
            {
                int exist = 0;
                foreach (var itemtag in itemMT.localTagList)
                {
                    foreach (string tag in tags)
                    {
                        if (string.Equals(itemtag.Name.ToLower(), tag.ToLower(), StringComparison.CurrentCultureIgnoreCase))
                        {
                            exist++;
                            break;
                        }
                    }
                    if (exist == tags.Length)
                    {
                        tempGOList.Add(itemMT.gameObject);
                        break;
                    }
                }
            }
        }

        if (tempGOList.Count > 0)
        {
            return tempGOList.ToArray();
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Search gameobjects with tags extension (only is false).
    /// </summary>
    public static GameObject[] FindGameObjectsWithMultiTags(params string[] tags)
    {
        return FindGameObjectsWithMultiTags(false, tags);
    }

    /// <summary>
    /// Search gameobject with tags extension (only = gameobject with ONLY specifc tags).
    /// </summary>
    public static GameObject FindGameObjectWithMultiTags(bool only, params string[] tags)
    {
        GameObject[] objects = FindGameObjectsWithMultiTags(only, tags);
        if (objects != null)
        {
            return FindGameObjectsWithMultiTags(only, tags)[0];
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Search gameobjects with tags extension (only is false).
    /// </summary>
    public static GameObject FindGameObjectWithMultiTags(params string[] tags)
    {
        return FindGameObjectWithMultiTags(false, tags);
    }

}

public static class MultiTagsHelperMethods
{

    /// <summary>
    /// Have these tags in GameObject
    /// </summary>
    public static bool HaveTags(this GameObject value, params string[] tags)
    {
        MultiTags CurrentGameComponent = value.GetComponent<MultiTags>();
        if (CurrentGameComponent == null || tags == null || tags.Length == 0)
        {
            return false;
        }

        bool aux = false;

        foreach (string tag in tags)
        {
            aux = false;
            foreach (var item in CurrentGameComponent.localTagList)
            {
                if (string.Equals(item.Name.ToLower(), tag.ToLower(), StringComparison.CurrentCultureIgnoreCase))
                {
                    aux = true;
                    break;
                }
            }

            if (!aux)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Return these tags in GameObject
    /// </summary>
    public static string[] ReturnTags(this GameObject value)
    {
        MultiTags multiTags = value.GetComponent<MultiTags>();
        if (multiTags && multiTags.localTagList.Count > 0)
        {
            List<MT> mts = multiTags.localTagList;
            string[] tags = new string[mts.Count];
            for (int i = 0; i < tags.Length; i++)
            {
                tags[i] = mts[i].Name;
            }
            return tags;
        }
        else return null;
    }

    /// <summary>
    /// Add these tags in GameObject
    /// </summary>
    public static void AddTags(this GameObject value, params string[] tags)
    {
        MultiTags CurrentGameComponent = value.GetComponent<MultiTags>();

        if (CurrentGameComponent == null)
        {
            value.AddComponent<MultiTags>();
            CurrentGameComponent = value.GetComponent<MultiTags>();
        }

        foreach (string tag in tags)
        {
            if (!HasTagPrivate(CurrentGameComponent, tag))
            {
                MT newItem = new MT();
                newItem.Name = tag;
                CurrentGameComponent.localTagList.Add(newItem);
            }
        }
    }

    /// <summary>
    /// Remove these tags in GameObject
    /// </summary>
    public static void RemoveTags(this GameObject value, params string[] tags)
    {
        MultiTags CurrentGameComponent = value.GetComponent<MultiTags>();
        if (CurrentGameComponent == null)
        {
            return;
        }

        foreach (string tag in tags)
        {
            MT tempItem = GetTagItem(CurrentGameComponent, tag);
            if (tempItem != null)
            {
                CurrentGameComponent.localTagList.Remove(tempItem);
            }
        }
    }

    //HAS TAG Private
    private static bool HasTagPrivate(MultiTags go, string tagToCheck)
    {
        foreach (var item in go.localTagList)
        {
            if (string.Equals(item.Name, tagToCheck, StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }

    //Private GetTagItem
    private static MT GetTagItem(MultiTags CGC, string tagToCheck)
    {
        foreach (var item in CGC.localTagList)
        {

            if (string.Equals(item.Name, tagToCheck, StringComparison.CurrentCultureIgnoreCase))
            {
                return item;
            }
        }
        return null;
    }
}

[System.Serializable] 
public class MT
{
	public string Name;
	public byte ID;
}


