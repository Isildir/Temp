import { DataSource } from '@angular/cdk/table';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { CollectionViewer } from '@angular/cdk/collections';

export abstract class BaseDataSource<Type> implements DataSource<Type> {
    protected valuesSubject = new BehaviorSubject<Type[]>([]);
    protected loadingSubject = new BehaviorSubject<boolean>(false);
    protected countSubject = new BehaviorSubject<number>(0);

    public loading = this.loadingSubject.asObservable();
    public count = this.countSubject.asObservable();

    connect(collectionViewer: CollectionViewer): Observable<Type[]> {
        return this.valuesSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.valuesSubject.complete();
        this.loadingSubject.complete();
        this.countSubject.complete();
    }
}