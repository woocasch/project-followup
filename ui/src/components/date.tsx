import styled from '@emotion/styled';
import { theme } from '@root/theme';
import React, { useId } from 'react';
import { FieldLabel } from './headers';

export interface DateInputProps {
  hiddenValue?: boolean;
  placeholder?: string;
  label: string;
  error?: string;
}

const FieldWrapper = styled.div(`
  display: contents;
`);

const InputStyled = styled.input(`
  background-color: ${theme.colors.primary};
  border-style: ridge;
  border-width: 1px;
`);

const ErrorDisplay = styled.div(`
    color: red;
    font-size: 0.9em;
`);

export const DateInput = React.forwardRef<
  HTMLInputElement,
  DateInputProps & React.InputHTMLAttributes<HTMLInputElement>
>(({ hiddenValue, placeholder, label, error, ...props }, ref) => {
  const id = useId();
  return (
    <FieldWrapper>
      {label && <FieldLabel id={id} text={label} />}
      <InputStyled
        id={id}
        type="date"
        ref={ref}
        placeholder={placeholder}
        {...props}
      />
      {error && <ErrorDisplay className="error">{error}</ErrorDisplay>}
    </FieldWrapper>
  );
});
