export interface GetLinkCodeDataParams {
    linkCode: string;
}

export interface LinkCodeData {
    emailAddress: string;
    displayName: string;
    isUsed: boolean;
}

export interface AccountsApi {
    getLinkCodeData(params: GetLinkCodeDataParams): Promise<LinkCodeData>;
}