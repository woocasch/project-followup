export interface CreateProjectRequest {
  title: string;
  description: string;
}

export interface CreateProjectResponse {
  success: boolean;
}

export interface CreateProjectService {
  createProject(request: CreateProjectRequest): Promise<CreateProjectResponse>;
}
