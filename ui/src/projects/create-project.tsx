import { Button, MultilineText, PageHeader, Text } from '@components/index';
import styled from '@emotion/styled';
import { theme } from '@root/theme';
import { useState } from 'react';
import { useNavigate } from 'react-router';
import type * as model from './create-project/create-project.model';
import createProjectService from './create-project/create-project.service';

const CreateProjectForm = styled.div(`
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

export default function CreateProject() {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const navigate = useNavigate();

  function onCreateClick() {
    const request: model.CreateProjectRequest = {
      title,
      description,
    };
    createProjectService.createProject(request).then((r) => {
      if (r.success) {
        navigate('/projects');
        return;
      }
    });
  }

  function onCancelClick() {
    navigate('/');
  }

  return (
    <div>
      <PageHeader>New project</PageHeader>
      <CreateProjectForm>
        <Text
          label="Project Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
        />
        <MultilineText
          label="Project Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
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
      </CreateProjectForm>
    </div>
  );
}
