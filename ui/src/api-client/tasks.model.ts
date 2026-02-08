export interface NewTaskData {
  title: string;
  description: string;
  dueDate?: Date;
}

export interface CreateTaskParameters {
    projectId: string;
    taskDetails: NewTaskData;
}

export interface CreateTaskResult {
    taskId: string;
}

export interface TasksApi {
    createTask(parameters: CreateTaskParameters): Promise<CreateTaskResult>;
}
