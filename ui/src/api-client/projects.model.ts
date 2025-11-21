export interface ProjectListItem {
  id: string;
  title: string;
  description: string;
  usersCount: number;
  tasksCompleted: number;
  tasksTotal: number;
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

export interface ProjectsApi {
  fetchProjectsList(): Promise<FetchListResult>;

  getProject(parameters: GetProjectParameters): Promise<GetProjectResult>;

  create(payload: CreatePayloadParameters): Promise<boolean>;

  edit(payload: EditPayloadParameters): Promise<boolean>;
}
