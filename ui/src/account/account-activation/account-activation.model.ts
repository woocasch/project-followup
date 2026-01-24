export interface LinkCodeData {
  emailAddress: string;
  displayName: string;
  isUsed: boolean;
}

export interface AccountActivationService {
  getLinkCodeData(linkCode: string): Promise<LinkCodeData>;
}
