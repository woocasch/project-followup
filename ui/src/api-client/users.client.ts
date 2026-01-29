import { ApiClientBase } from '../api-client/api-client-base';
import type * as model from './users.model';

export class UsersClient extends ApiClientBase implements model.UsersApi {
  async createUser(
    parameters: model.CreateUserParameters,
  ): Promise<model.CreateUserResult> {
    const response = await this.createBffClient().post('api/Users', parameters);

    if (response.status !== 201) {
      return { created: false };
    }

    return { created: true };
  }
}

const usersApi: model.UsersApi = new UsersClient();

export default usersApi;
