import { ApiClientBase } from '../api-client/api-client-base';
import type * as model from './accounts.model';

export class AccountsClient extends ApiClientBase implements model.AccountsApi {
  async getLinkCodeData(
    params: model.GetLinkCodeDataParams,
  ): Promise<model.LinkCodeData> {
    const response = await this.createBffClient().get(
      `api/users/linkCodes/${params.linkCode}`,
    );
    return response.data;
  }
}

const accountsApi: model.AccountsApi = new AccountsClient();

export default accountsApi;
