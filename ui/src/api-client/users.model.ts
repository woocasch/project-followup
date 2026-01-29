export interface CreateUserParameters {
  email: string;
  displayName: string;
}

export interface CreateUserResult {
  created: boolean;
}

export interface UsersApi {
  createUser(parameters: CreateUserParameters): Promise<CreateUserResult>;
}
