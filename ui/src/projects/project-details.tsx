import { NamedPanel, PageHeader } from "@root/components";
import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router';
import type * as model from './edit-project/edit-project.model';
import editProjectService from './edit-project/edit-project.service';
import styled from "@emotion/styled";

type RouteParams = Record<'projectId', string>;

const ContentStyled = styled.div(`
  margin: auto;
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  gap: 1em;
  .description {
    grid-column: 1 / 3;}
`);

export default function ProjectDetails() {
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

  return (
    <div style={{ textAlign: 'center' }}>
      <PageHeader>{title}</PageHeader>
      <ContentStyled>
        <div className="description">{description}</div>
        <NamedPanel title='Assigned users'>
          <p>List of assigned users</p>
        </NamedPanel>
        <NamedPanel title='Tasks'>
          <p>List of tasks</p>
        </NamedPanel>
      </ContentStyled>
    </div>
  );
}