import styled from '@emotion/styled';
import { Button, PageHeader, Text } from '@root/components';
import { theme } from '@root/theme';
import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router';
import type * as model from './edit-project/edit-project.model';
import editProjectService from './edit-project/edit-project.service';

const EditProjectForm = styled.div(`
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

type RouteParams = Record<'projectId', string>;

export default function EditProject() {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const { projectId } = useParams<RouteParams>();
  const navigate = useNavigate();

  useEffect(() => {
    if (!projectId) {
      navigate('/');
      return;
    }

    const request: model.GetProjectRequest = {
      projectId: projectId,
    };

    editProjectService.getProject(request).then((r) => {
      if (r.project) {
        setTitle(r.project.title);
        setDescription(r.project.description);
      }
    });
  }, [projectId, navigate]);

  async function onEditClick() {
    const request: model.EditProjectRequest = {
      projectId: projectId ?? '',
      title,
      description,
    };

    const response = await editProjectService.editProject(request);
    if (response.success) {
      navigate('/projects');
      return;
    }
  }

  function onCancelClick() {
    navigate('/');
  }

  return (
    <div>
      <PageHeader>Edit project</PageHeader>
      <EditProjectForm>
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
          <Button variant="action" buttonType="rounded" onClick={onEditClick}>
            Update
          </Button>
          <Button
            variant="warning"
            buttonType="rounded"
            onClick={onCancelClick}
          >
            Cancel
          </Button>
        </ButtonsContainer>
      </EditProjectForm>
    </div>
  );
}
