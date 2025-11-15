export interface ProjectRole {
  id: string;
  name: string;
}

export interface ProjectMember {
  id: string;
  name: string;
  roles: ProjectRole[];
}

export interface ProjectListItem {
  id: string;
  name: string;
  description: string;
  usersCount: number;
  tasksCompleted: number;
  tasksTotal: number;
}

export interface ProjectsListService {
  getProjectsList(): Promise<ProjectListItem[]>;
}
