import accountActivationService from './account-activation/account-activation.service';
import { z } from 'zod';

const activationFormSchema = z.object({
    password: z.string().min(8, 'Password must be at least 8 characters long'),
    repeatPassword: z.string(),
}).superRefine(({ repeatPassword, password }, ctx) => {
  if (repeatPassword !== password) {
    ctx.addIssue({
      code: "custom",
      message: "The passwords did not match",
      path: ['repeatPassword']
    });
  }
});

export interface SetupActivationComponentInput {
  linkCode: string;
  navigate: (path: string) => void;
  setDisplayName: (name: string) => void;
  setEmailAddress: (email: string) => void;
}

export interface ActivateAccountInput {
    linkCode: string;
    password: string;
}

export class ActivateAccountLogic {
  async setupActivationComponent(input: SetupActivationComponentInput) {
    if (!input.linkCode) {
      input.navigate('/');
      return;
    }

    accountActivationService.getLinkCodeData(input.linkCode).then((r) => {
      if (r) {
        input.setDisplayName(r.displayName);
        input.setEmailAddress(r.emailAddress);
      }
    });
  }

  async activateAccount(input: ActivateAccountInput) {
    return new Promise<void>((resolve) => {
        console.log('Activating account with', input);
        resolve();
    })
  }

  getFormSchema() {
    return activationFormSchema;
  }
}

const activateAccountLogic: ActivateAccountLogic = new ActivateAccountLogic();

export default activateAccountLogic;
