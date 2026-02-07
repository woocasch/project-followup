export interface ProjectDetails {
  id: string;
  title: string;
  description: string;
}

export interface UserData {
  id: string;
  displayName: string;
}

export interface TaskData {
  id: string;
  title: string;
  status: string;
}

export interface GetProjectRequest {
  projectId: string;
}

export interface GetProjectResponse {
  project: ProjectDetails | null;
}

export interface FetchProjectUsersRequest {
  projectId: string;
}

export interface FetchProjectUsersResponse {
  users: UserData[];
}

export interface FetchProjectTasksRequest {
  projectId: string;
}

export interface FetchProjectTasksResponse {
  tasks: TaskData[];
}

export interface ProjectDetailsService {
  getProject(request: GetProjectRequest): Promise<GetProjectResponse>;
  fetchProjectUsers(
    request: FetchProjectUsersRequest,
  ): Promise<FetchProjectUsersResponse>;
  fetchProjectTasks(
    request: FetchProjectTasksRequest,
  ): Promise<FetchProjectTasksResponse>;
}
