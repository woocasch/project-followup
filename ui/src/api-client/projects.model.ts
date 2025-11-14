export interface ProjectListItem {
  id: string;
  title: string;
  description: string;
  membersCount: number;
  tasksCompleted: number;
  tasksTotal: number;
}

export interface FetchListResult {
  projects: ProjectListItem[];
}

export interface CreatePayload {
  title: string;
  description: string;
}

export interface ProjectsApi {
  fetchProjectsList(): Promise<FetchListResult>;

  create(payload: CreatePayload): Promise<boolean>;
}
