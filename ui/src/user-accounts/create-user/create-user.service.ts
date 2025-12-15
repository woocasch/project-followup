import usersApi from "@root/api-client/users.client";
import * as model from "./create-user.model";

export class CreateUserWebService implements model.CreateUserService {
    async createUser(request: model.CreateUserRequest): Promise<model.CreateUserResponse> {
        return usersApi.createUser(request);
    }
}

const createUserService: model.CreateUserService = new CreateUserWebService();

export default createUserService;