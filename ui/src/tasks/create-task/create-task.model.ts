import type * as apiModel from '@apiClient/projects.model';

export interface AssignedUser {
  id: string;
  displayName: string;
}

export interface NewTaskData {
  title: string;
  description: string;
  dueDate?: string;
}

export interface TaskDetails {
  id: string;
  title: string;
  description: string;
  dueDate?: Date;
  status: apiModel.ProjectTaskStatus;
  assignedUsers: AssignedUser[];
}

export interface CreateTaskRequest {
  projectId: string;
  taskDetails: NewTaskData;
}

export interface CreateTaskResponse {
  taskId?: string;
}

export interface CreateTaskService {
  createTask(request: CreateTaskRequest): Promise<CreateTaskResponse>;
}
