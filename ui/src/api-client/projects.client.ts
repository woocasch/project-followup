import axios from 'axios';
import type * as model from './projects.model';

interface EditPayload {
  title: string;
  description: string;
}

interface GetProjectOutput {
  projectId: string;
  title: string;
  description: string;
}

export class ProjectsClient implements model.ProjectsApi {
  async fetchProjectsList(): Promise<model.FetchListResult> {
    const response = await axios.get<model.FetchListResult>(
      `https://localhost:7037/api/Projects`,
    );
    if (response.status !== 200) {
      return { projects: [] };
    }
    return response.data;
  }

  async getProject(
    parameters: model.GetProjectParameters,
  ): Promise<model.GetProjectResult> {
    try {
      const response = await axios.get<GetProjectOutput>(
        `https://localhost:7037/api/Projects/${parameters.projectId}`,
      );

      return {
        project: {
          id: response.data.projectId,
          title: response.data.title,
          description: response.data.description,
        }
      };
    } catch (error) {
      if (axios.isAxiosError(error) && error.response?.status === 404) {
        return { project: null };
      }
      throw error;
    }
  }

  async create(payload: model.CreatePayloadParameters): Promise<boolean> {
    const response = await axios.post(
      `https://localhost:7037/api/Projects`,
      payload,
    );
    return response.status === 201;
  }

  async edit(payload: model.EditPayloadParameters): Promise<boolean> {
    const response = await axios.put(
      `https://localhost:7037/api/Projects/${payload.projectId}`,
      <EditPayload>{
        title: payload.title,
        description: payload.description,
      },
    );
    return response.status === 202;
  }
}

const projectsApi: model.ProjectsApi = new ProjectsClient();

export default projectsApi;
