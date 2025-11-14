import { Button, PageHeader, Text } from '@components/index';
import styled from '@emotion/styled';
import { theme } from '@root/theme';
import { useState } from 'react';

const CreateProjectForm = styled.div(`
  display: grid;
  grid-template-columns: 135px auto;
  gap: 1em;
  align-items: center;
  width: 50%;
  max-width: 50%;
  margin: auto;

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

export default function CreateProject() {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');

  return (
    <div>
      <PageHeader>New project</PageHeader>
      <CreateProjectForm>
        <Text
          value={title}
          setValue={(v) => setTitle(v)}
          label="Project Title"
        />
        <Text
          multiline={true}
          value={description}
          setValue={(v) => setDescription(v)}
          label="Project Description"
        />
        <ButtonsContainer>
          <Button variant="action" buttonType="rounded">
            Create
          </Button>
          <Button variant="warning" buttonType="rounded">
            Cancel
          </Button>
        </ButtonsContainer>
      </CreateProjectForm>
    </div>
  );
}
