import * as model from './account-activation.model';
import accountsApi from '@root/api-client/accounts.client';

export class AccountActivationWebService implements model.AccountActivationService {
    async getLinkCodeData(linkCode: string): Promise<model.LinkCodeData> {
        return accountsApi.getLinkCodeData({ linkCode });
    }
}

const accountActivationService: model.AccountActivationService = new AccountActivationWebService();

export default accountActivationService;
