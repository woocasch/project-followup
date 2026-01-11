import { Button, PageHeader, Text } from '@components/index';
import styled from '@emotion/styled';
import { theme } from '@root/theme';
import { useState } from 'react';
import { useNavigate } from 'react-router';
import createUserService from './create-user/create-user.service';

const CreateUserForm = styled.div(`
  margin: auto;
  margin-top: ${theme.spaces.large};
  display: grid;
  grid-template-columns: 135px auto;
  gap: 1em;
  align-items: center;
  width: 50%;
  max-width: 50%;

  &>label {
    text-align: right;
  }
`);

const ButtonsContainer = styled.div(`
    grid-column: span 2;
    text-align: center;
    &>* {
    margin: ${theme.spaces.xsmall};
`);

export default function CreateUser() {
  const [email, setEmail] = useState('');
  const [displayName, setDisplayName] = useState('');
  const navigate = useNavigate();

  async function onCreateClick() {
    const result = await createUserService.createUser({ email, displayName });
    if (result.created) {
      navigate('/users');
    }
  }

  function onCancelClick() {
    navigate('/');
  }
  return (
    <div>
      <PageHeader>New user</PageHeader>
      <CreateUserForm>
        <Text value={email} setValue={(v) => setEmail(v)} label="Email" />
        <Text
          value={displayName}
          setValue={(v) => setDisplayName(v)}
          label="Display Name"
        />
        <ButtonsContainer>
          <Button variant="action" buttonType="rounded" onClick={onCreateClick}>
            Create
          </Button>
          <Button
            variant="warning"
            buttonType="rounded"
            onClick={onCancelClick}
          >
            Cancel
          </Button>
        </ButtonsContainer>
      </CreateUserForm>
    </div>
  );
}
