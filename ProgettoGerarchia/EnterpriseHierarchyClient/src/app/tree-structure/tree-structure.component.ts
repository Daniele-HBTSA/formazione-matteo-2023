import { NestedTreeControl } from "@angular/cdk/tree";
import { Component } from "@angular/core";
import { MatTreeNestedDataSource } from "@angular/material/tree";
import { TreeService } from '../services/tree.service';
import { EnterprieseTree } from "../model/EnterprieseTree";
import { MatDialog } from "@angular/material/dialog";
import { DialogComponent } from "../dialog/dialog.component";

@Component({
  selector: 'app-tree-structure',
  templateUrl: './tree-structure.component.html',
  styleUrls: ['./tree-structure.component.css'],
})
export class TreeStructureComponent{

  dataSource = new MatTreeNestedDataSource<EnterprieseTree>();
  treeControl = new NestedTreeControl<EnterprieseTree>(node => node.Children);

  constructor(private treeService: TreeService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.treeService.GetTreeFromDB().subscribe({
      next: (tree: EnterprieseTree[]) => {
        if (tree != null) 
          this.dataSource.data = tree;
      },
      error(err) {
        alert("Tree is null");
      }
    });
  };

  hasChild = (_: number, node: EnterprieseTree) => !!node.Children && node.Children.length > 0;

  isSelected(node : EnterprieseTree) {
    if (node.Selected)
      return "mat-tree-node is-selected";
    else 
      return "mat-tree-node"
  }

  checkSingle(enterpriseId : number) {
    this.treeService.SelectSingleEnterprise(enterpriseId).subscribe({
      next: (tree: EnterprieseTree[]) => {
        if (tree != null)
          this.dataSource.data = tree;
      },
      error(err) {
        alert("Tree is null");
      }
    });
  }

  selectTree(fatherId : number) {
    this.treeService.SelectWholeTree(fatherId).subscribe({
      next: (tree: EnterprieseTree[]) => {
        if (tree != null)
          this.dataSource.data = tree;
      },
      error(err) {
        alert("Tree is null");
      }
    });
  }

  openDialog(fatherId :number): void {
    const dialogRef = this.dialog.open(DialogComponent, {
      data : fatherId
    });

    dialogRef.afterClosed().subscribe(result => {
      this.dataSource.data = result;
    });
  }


}