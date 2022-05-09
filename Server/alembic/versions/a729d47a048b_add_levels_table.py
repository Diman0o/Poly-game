"""add_levels_table

Revision ID: a729d47a048b
Revises: 5f0b969e65b7
Create Date: 2022-05-09 20:13:05.537607

"""
from alembic import op
import sqlalchemy as sa


# revision identifiers, used by Alembic.
revision = 'a729d47a048b'
down_revision = '5f0b969e65b7'
branch_labels = None
depends_on = None


def upgrade():
    op.create_table(
    'level_pass',
    sa.Column('id', sa.Integer, primary_key=True),
    sa.Column('accountId', sa.String, sa.ForeignKey("account.id"), nullable=False),
    sa.Column('levelName', sa.String, nullable=False))


def downgrade():
    op.drop_table('level_pass')
