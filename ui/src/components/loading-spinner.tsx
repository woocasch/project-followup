import { css, keyframes } from '@emotion/react';
import styled from '@emotion/styled';
import { theme } from '../theme';

const spin = keyframes`
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
`;

const LoadingContainer = styled.div(`
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  min-height: 200px;
  gap: ${theme.spaces.medium};
`);

const Spinner = styled.div(css`
  border: 3px solid ${theme.colors.border};
  border-top: 3px solid ${theme.colors.primary};
  border-radius: 50%;
  width: 32px;
  height: 32px;
  animation: ${spin} 1s linear infinite;
`);

const LoadingText = styled.div(`
  color: ${theme.colors.textSecondary};
  font-size: ${theme.fontSizes.medium};
`);

interface LoadingSpinnerProps {
  message?: string;
}

export default function LoadingSpinner({
  message = 'Loading...',
}: LoadingSpinnerProps) {
  return (
    <LoadingContainer>
      <Spinner />
      <LoadingText>{message}</LoadingText>
    </LoadingContainer>
  );
}
