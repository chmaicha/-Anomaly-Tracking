/**
 * Interface that describes the triggered exception fields.
 */
export interface IException {

  Message?: string;
  MessageKey?: string;
  InnerException?: string;
  ErrorneousEntity?: string;
}

/**
 * Interface that encapsulates data received from web service call.
 */
export interface IResponse<T> extends IException {

  Data: T;
  IsSuccessful: boolean;
  TotalCount?: number;
}
