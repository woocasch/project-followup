import styled from '@emotion/styled';
import { Button, PageHeader, Password } from '@root/components';
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { theme } from '@root/theme';
import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router';
import activateAccountLogic from './activate-account.logic';

const DisplayContainer = styled.div(`
    margin: auto;
    width: 50%;
    display: grid;
    grid-template-columns: 1fr 1fr;
    &>*.description {
        grid-column: span 2;
    }
    gap: ${theme.spaces.large};
`);

const Header = styled.p(`
    font-size: ${theme.fontSizes.large};
    font-weight: bold;
    text-align: center;
`);

const UserDatum = styled.span(`
    font-style: italic;
`);

const Buttons = styled.div(`
    grid-column: span 2;
    text-align: center;
`);

type RouteParams = Record<'linkCode', string>;

export default function ActivateAccount() {
  const { linkCode } = useParams<RouteParams>();
  const navigate = useNavigate();

  const [displayName, setDisplayName] = useState('');
  const [emailAddress, setEmailAddress] = useState('');
//   const [password, setPassword] = useState('');
//   const [repeatPassword, setRepeatPassword] = useState('');
  const form = useForm({
    resolver: zodResolver(activateAccountLogic.getFormSchema()),
  });

  useEffect(() => {
    activateAccountLogic.setupActivationComponent({
      linkCode: linkCode || '',
      navigate,
      setDisplayName,
      setEmailAddress,
    });
  }, [linkCode, navigate]);

  async function onSubmit() {
    const input = {
        linkCode: linkCode || '',
        password: form.getValues('password'),
    };
    await activateAccountLogic.activateAccount(input);
  }

  return (
    <DisplayContainer>
      <PageHeader className="description">Account activation</PageHeader>
      <Header className="description">
        Hello <UserDatum>{displayName}</UserDatum>
      </Header>
      <div className="description">
        You were invited to <strong>Project Follow-Up</strong> application
        through e-mail address <UserDatum>{emailAddress}</UserDatum>
      </div>
      <div className="description">Select a password to activate your account:</div>
      <Password
        label="Password"
        {...form.register('password')}
        error={form.formState.errors.password?.message}
      />
      <Password
        label="Repeat password"
        {...form.register('repeatPassword')}
        error={form.formState.errors.repeatPassword?.message}
      />
      <Buttons>
        <Button
          variant="button"
          buttonType="rounded"
          onClick={() => form.handleSubmit(onSubmit)()}
        >
          Activate account
        </Button>
      </Buttons>
    </DisplayContainer>
  );
}
