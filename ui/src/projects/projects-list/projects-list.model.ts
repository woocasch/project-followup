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
  membersCount: number;
}

export interface ProjectsListService {
  getProjectsList(): Promise<ProjectListItem[]>;
}
