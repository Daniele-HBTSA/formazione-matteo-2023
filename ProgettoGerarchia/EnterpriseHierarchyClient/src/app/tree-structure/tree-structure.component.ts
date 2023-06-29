import { Component, OnInit } from '@angular/core';
import { TreeService } from '../services/tree.service';
import { EnterprieseTree } from '../model/EnterprieseTree';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { NestedTreeControl } from '@angular/cdk/tree';

@Component({
  selector: 'app-tree-structure',
  templateUrl: './tree-structure.component.html',
  styleUrls: ['./tree-structure.component.css'],
})
export class TreeStructureComponent implements OnInit{

  dataSource = new MatTreeNestedDataSource<EnterprieseTree>();
  treeControl = new NestedTreeControl<EnterprieseTree>(node => node.Children);

  constructor(private treeStruct: TreeService) { }

  ngOnInit(): void {
    this.treeStruct.GetTreeFromDB().subscribe({
      next: (tree: EnterprieseTree[]) => {
        if (tree != null) {
          this.dataSource.data = tree;
          console.log(tree)
        }
      },
      error(err) {
        alert("Tree is null");
      }
    });
  };

  hasChild = (_: number, node: EnterprieseTree) => !!node.Children && node.Children.length > 0;
}
