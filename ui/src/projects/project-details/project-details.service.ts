import type * as model from './project-details.model';


export class ProjectDetailsWebService implements model.ProjectDetailsService {
    getProject(request: model.GetProjectRequest): Promise<model.GetProjectResponse> {
        return new Promise((resolve) => {
            setTimeout(() => {
                resolve({
                    project: {
                        id: request.projectId,
                        title: 'Project Title',
                        description: 'Project Description',
                    },
                });
            }, 1000);
        });
    }

    fetchProjectUsers(_: model.FetchProjectUsersRequest): Promise<model.FetchProjectUsersResponse> {
        return new Promise((resolve) => {
            const delay = Math.random() * 2000 + 800; // Simulate network delay between 800 and 2800ms
            setTimeout(() => {
                resolve({
                    users: [
                        { id: '1', displayName: 'User 1' },
                        { id: '2', displayName: 'User 2' },
                    ],
                });
            }, delay);
        });
    }

    fetchProjectTasks(_: model.FetchProjectTasksRequest): Promise<model.FetchProjectTasksResponse> {
        return new Promise((resolve) => {
            const delay = Math.random() * 2000 + 800; // Simulate network delay between 800 and 2800ms
            setTimeout(() => {
                resolve({
                    tasks: [
                        { id: '1', title: 'Task 1', status: 'Open' },
                        { id: '2', title: 'Task 2', status: 'In Progress' },
                    ],
                });
            }, delay);
        });
    }
}

const projectDetailsService: model.ProjectDetailsService = new ProjectDetailsWebService();

export default projectDetailsService;