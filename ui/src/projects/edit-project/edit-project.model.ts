export interface ProjectDetails {
  id: string;
  title: string;
  description: string;
}

export interface GetProjectRequest {
  projectId: string;
}

export interface GetProjectResponse {
  project: ProjectDetails | null;
}

export interface EditProjectRequest {
  projectId: string;
  title: string;
  description: string;
}

export interface EditProjectResponse {
  success: boolean;
}

export interface EditProjectService {
  getProject(request: GetProjectRequest): Promise<GetProjectResponse>;

  editProject(request: EditProjectRequest): Promise<EditProjectResponse>;
}
