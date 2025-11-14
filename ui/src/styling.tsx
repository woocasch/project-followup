import styled from '@emotion/styled';
import { Calendar } from 'lucide-react';
import { Button, Card, CardSize, IconButton, PageHeader } from './components';
import { theme } from './theme';

const CardsContainer = styled.div(`
    display: flex;
    flex-direction: row;
    gap: ${theme.spaces.medium};
    & > div {
        flex: 0 0 23%;
    }
`);

export default function StylingPage() {
  return (
    <div>
      <PageHeader>Styling guidelines</PageHeader>
      <p>This page will contain styling guidelines for the project.</p>
      <div>
        <h3>Cards</h3>
        <CardsContainer>
          <Card size={CardSize.Small}>
            <Card.Header>
              <h2>Card Small</h2>
            </Card.Header>
            <Card.Content>
              <p>This is the card content</p>
            </Card.Content>
            <Card.Footer>
              <Button onClick={() => alert('Button clicked!')}>
                Variant: Button
              </Button>
              <Button
                variant="warning"
                onClick={() => alert('Warning clicked!')}
              >
                Variant: Warning
              </Button>
            </Card.Footer>
          </Card>
          <Card size={CardSize.Medium}>
            <Card.Header>
              <h2>Card Medium</h2>
            </Card.Header>
            <Card.Content>
              <p>This is the card content</p>
            </Card.Content>
            <Card.Footer>
              <Button
                variant="success"
                onClick={() => alert('Success clicked!')}
              >
                Variant: Success
              </Button>
              <IconButton
                icon={Calendar}
                onClick={() => alert('Calendar clicked!')}
              />
            </Card.Footer>
          </Card>
          <Card size={CardSize.Large}>
            <Card.Header>
              <h2>Card Large</h2>
            </Card.Header>
            <Card.Content>
              <p>This is the card content</p>
            </Card.Content>
            <Card.Footer>
              <Button variant="error" onClick={() => alert('Error clicked!')}>
                Variant: Error
              </Button>
            </Card.Footer>
          </Card>
        </CardsContainer>
      </div>
    </div>
  );
}
