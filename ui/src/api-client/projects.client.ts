import * as model from 'projects.model';
import axios from 'axios';

export class ProjectsClient implements model.ProjectsApi {
    async fetchProjectsList(): Promise<model.FetchListResult> {
        const response = await axios.get<model.FetchListResult>(
            `https://localhost:7037/api/Projects`);
            if (response.status !== 200) {
                return { projects: [] };
            }
            return response.data;
    }

    async create(payload: model.CreatePayload): Promise<boolean> {
        const response = await axios.post(
            `https://localhost:7037/api/Projects`, payload);
        return response.status === 202;
    }
}

export const projectsApi: model.ProjectsApi = new ProjectsClient();