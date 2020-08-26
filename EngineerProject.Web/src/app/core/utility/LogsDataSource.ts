import { Log } from '../models/Log';
import { LogsDataService } from '../services/data/logs-data.service';
import { of } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { FilteredTableModel } from '../models/LogTable';
import { BaseDataSource } from './BaseDataSource';

export class LogsDataSource extends BaseDataSource<Log> {
    constructor(private logsService: LogsDataService) {
        super();
    }

    loadPage(
        pageIndex: number,
        pageSize: number,
        sortProperty?: string,
        sortOrder?: string,
        clientId?: number,
        startDate?: Date,
        endDate?: Date) {
        this.loadingSubject.next(true);

        this.logsService.getLogs(pageIndex, pageSize, sortProperty, sortOrder, clientId, startDate, endDate).pipe(
            catchError(() => of([])),
            finalize(() => this.loadingSubject.next(false))
        )
        .subscribe(response => {
            const parsedResponse = (response as FilteredTableModel);

            this.valuesSubject.next(parsedResponse.Data);
            this.countSubject.next(parsedResponse.Count);
        });
    }
}