import * as model from './users.model';
import { ApiClientBase } from '../api-client/api-client-base';

export class UsersClient extends ApiClientBase implements model.UsersApi {
    async createUser(parameters: model.CreateUserParameters): Promise<model.CreateUserResult> {
        const response = await this.createBffClient().post(
            'api/Users',
            parameters
        );

        if (response.status !== 201) {
            return { created: false };
        }

        return { created: true };
    }
}

const usersApi: model.UsersApi = new UsersClient();

export default usersApi;
