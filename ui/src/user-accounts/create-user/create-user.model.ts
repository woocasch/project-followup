export interface CreateUserRequest {
  email: string;
  displayName: string;
}

export interface CreateUserResponse {
  created: boolean;
}

export interface CreateUserService {
  createUser(request: CreateUserRequest): Promise<CreateUserResponse>;
}
