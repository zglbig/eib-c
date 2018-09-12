using BZFrameWork;
using org.bql.logic.hall.task;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasKInit : SingletonMono<TasKInit>
{
    //定义一个字典
    private  Dictionary<int, List<TaskDataTable>> taskTableDic = new Dictionary<int, List<TaskDataTable>>();
    // Use this for initialization
    private void Awake()
    {
        //定义一个存对象的字典
        Dictionary<int, object> dict = StaticConfigMessage.Instance.GetMapForType(typeof(TaskDataTable));
        //遍历这个字典
        foreach (object o in dict.Values)
        {
            //
            TaskDataTable c = (TaskDataTable)o;
            int taskType = c.TaskType;
            if (taskTableDic.ContainsKey(taskType))
            {
                taskTableDic[taskType].Add(c);
            }
            else
            {
                List<TaskDataTable> l = new List<TaskDataTable>();
                l.Add(c);
                taskTableDic[taskType] = l;
            }
        }
    }
    public  List<TaskDataTable> GetTaskTable(int taskType)
    {
        if (taskTableDic.ContainsKey(taskType))
            return taskTableDic[taskType];
        return null;
    }

    private void OnDestroy()
    {
        taskTableDic.Clear();
    }
}
