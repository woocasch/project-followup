export interface ProjectListItem {
  id: string;
  title: string;
  description: string;
  usersCount: number;
  tasksCompleted: number;
  tasksTotal: number;
}

export interface UserData {
  id: string;
  displayName: string;
}

export enum ProjectTaskStatus {
  Created = 'Created',
  InProgress = 'InProgress',
  Completed = 'Completed',
  Removed = 'Removed',
}

export interface TaskData {
  id: string;
  title: string;
  status: ProjectTaskStatus;
}

export interface FetchListResult {
  projects: ProjectListItem[];
}

export interface ProjectDetails {
  id: string;
  title: string;
  description: string;
}

export interface GetProjectParameters {
  projectId: string;
}

export interface GetProjectResult {
  project: ProjectDetails | null;
}

export interface CreatePayloadParameters {
  title: string;
  description: string;
}

export interface EditPayloadParameters {
  projectId: string;
  title: string;
  description: string;
}

export interface FetchProjectUsersParameters {
  projectId: string;
}

export interface FetchProjectUsersResult {
  users: UserData[];
}

export interface FetchProjectTasksParameters {
  projectId: string;
}

export interface FetchProjectTasksResult {
  tasks: TaskData[];
}

export interface ProjectsApi {
  fetchProjectsList(): Promise<FetchListResult>;

  getProject(parameters: GetProjectParameters): Promise<GetProjectResult>;

  create(payload: CreatePayloadParameters): Promise<boolean>;

  edit(payload: EditPayloadParameters): Promise<boolean>;

  fetchProjectUsers(
    parameters: FetchProjectUsersParameters,
  ): Promise<FetchProjectUsersResult>;

  fetchProjectTasks(
    parameters: FetchProjectTasksParameters,
  ): Promise<FetchProjectTasksResult>;
}
