import styled from '@emotion/styled';
import { theme } from '@root/theme';
import React, { useId } from 'react';
import { FieldLabel } from './headers';

export interface MultilineTextProps {
  placeholder?: string;
  label: string;
  error?: string;
}

const FieldWrapper = styled.div(`
  display: contents;
`);

const InputStyled = styled.textarea(`
  background-color: ${theme.colors.primary};
  border-style: ridge;
  border-width: 1px;
`);

const ErrorDisplay = styled.div(`
    color: red;
    font-size: 0.9em;
`);

export const MultilineText = React.forwardRef<
  HTMLTextAreaElement,
  MultilineTextProps & React.TextareaHTMLAttributes<HTMLTextAreaElement>
>(({ placeholder, label, error, ...props }, ref) => {
  const id = useId();
  return (
    <FieldWrapper>
      {label && <FieldLabel id={id} text={label} />}
      <InputStyled id={id} ref={ref} placeholder={placeholder} {...props} />
      {error && <ErrorDisplay className="error">{error}</ErrorDisplay>}
    </FieldWrapper>
  );
});
