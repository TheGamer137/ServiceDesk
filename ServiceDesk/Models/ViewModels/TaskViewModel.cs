﻿namespace ServiceDesk.Models.ViewModels;

public class TaskViewModel
{
    public int TaskId { get; set; }
    public string TaskTitle { get; set; }
    public string TaskDescription { get; set; }
    public Client Client { get; set; }
    public Executor CurrentExecutor { get; set; }
    public List<Executor> Executors { get; set; }
    public DateTime Deadline { get; set; }
    public List<Status> StatusList { get; set; }
    public string Comment { get; set; }
}