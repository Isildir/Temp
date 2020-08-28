export interface GenericRequestResult<T> {
    isSuccessful: boolean;
    data?: T;
    errorMessage?: string;
}
